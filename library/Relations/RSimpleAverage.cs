using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm.Relations
{
    /// <summary>
    /// Represents MISO relations between preceding concepts and single following concept. (MISO - multiple input, single output)
    /// This class implements standard linear weighted FCM connection with averaging thresholding function.
    /// </summary>
    public class RSimpleAverage : Interfaces.IRelation
    {
        #region PROPERTIES & FIELDS
        //----------------------------------------------------//

        //connected previous concepts
        public List<Concept> previous { get; set; }

        //linear weights
        private List<double> weights;

        #endregion //-----------------------------------------//

        #region CONSTRUCTOR
        //----------------------------------------------------//

        /// <summary>
        /// Create new FCM relation
        /// </summary>
        public RSimpleAverage()
        {
            previous = new List<Concept>();
            weights = new List<double>();
        }

        #endregion //-----------------------------------------//

        #region METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Return relation model information.
        /// </summary>
        public string info()
        {
            return "Simple FCM relation defined by list of weights between previous concepts and folowing concept";
        }

        /// <summary>
        /// Attach new preceding concept to the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        public void attach(Concept concept)
        {
            int i = previous.FindIndex(c => c.name == concept.name);
            if (i == -1)
            {
                previous.Add(concept);
                weights.Add(1);
            }
        }

        /// <summary>
        /// Detach existing preceding concept from the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        public void detach(Concept concept)
        {
            int i = previous.FindIndex(c => c.name == concept.name);
            if (i >= 0)
            {
                previous.RemoveAt(i);
                weights.RemoveAt(i);
            }
        }

        /// <summary>
        /// Get string containing relations weights.
        /// </summary>
        /// <param name="parameters">optional preceding weights specified by list of concept names</param>
        /// <returns>string containing relation weights separated by semicolons</returns>
        public string get(params string[] parameters)
        {
            //return empty string if there are no weights
            if (weights.Count == 0)
                return "error - no weights";
            //return all weights if no concept is specified
            if (parameters == null || parameters.Count() == 0)
                return string.Join(";", weights);
            //return weights of only specified concepts
            List<string> toReturn = new List<string>(parameters.Count());
            for (int j = 0; j < parameters.Count(); j++)
            {
                int i = previous.FindIndex(c => c.name == parameters[j]);
                if (i == -1)
                    return "error - no such concept '" + parameters[j] + "'";
                else
                    toReturn.Add(weights[i].ToString());
            }
            return string.Join(";", toReturn);
        }

        /// <summary>
        /// Set relation using provided data
        /// <param name="parameters">string containing weights separated by semicolons or name of preceding concept and value of related weight</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int set(params string[] parameters)
        {
            //set specific single weight
            if (parameters.Count() == 2)
            {
                int i = previous.FindIndex(c => c.name == parameters[0]);
                if (i == -1)
                    return -1;
                weights[i] = Convert.ToDouble(parameters[1]);
                return 0;
            }
            //set all weights
            if (parameters.Count() == 1)
            {
                char[] delimiterChars = { ' ', ';', '\t' };
                string[] newStrings = parameters[0].Split(delimiterChars);
                double[] newWeights = new double[newStrings.Length];
                if (newStrings.Length != weights.Count)
                    return -1;
                for (int i = 0; i < newStrings.Length; i++)
                    newWeights[i] = Convert.ToDouble(newStrings[i]);
                weights = new List<double>(newWeights);
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Propagate inputs through relation and calculate new value for the following concept.
        /// </summary>
        public double propagate()
        {
            double sum = 0;
            for (int i = 0; i < previous.Count; i++)
            {
                sum += previous[i].value * weights[i];
            }
            return sum/previous.Count;
        }

        /// <summary>
        /// Error backpropagation
        /// </summary>
        /// <param name="delta">error signal delta</param>
        public void backprop(double delta)
        {
            for (int i = 0; i < previous.Count; i++)
            {
                previous[i].newDelta += delta * weights[i];
            }
        }

        /// <summary>
        /// Relation adaptation/learning via the "delta rule"
        /// </summary>
        /// <param name="delta">error delta</param>
        public void adapt(double delta, double gama)
        {
            for (int i = 0; i < previous.Count; i++)
            {
                weights[i] += gama * delta * previous[i].value;
            }
        }

        #endregion //-----------------------------------------//
    }
}
