using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm
{
    /// <summary>
    /// Represents MISO relations between preceding concepts and single following concept. (MISO - multiple input, single output)
    /// This class implements standard linear weighted FCM connection.
    /// </summary>
    public class RSimple : IRelation
    {
        #region PROPERTIES & FIELDS
        //----------------------------------------------------//

        //type of relation
        public string inputType { get; set; }   //simple, windowed, TTR
        public string procType { get; set; }    //linear, function, neural

        //connected concepts
        public Concept next { get; set; }              //following concept
        public List<Concept> previous { get; set; }    //preceding concepts

        //linear weights
        private List<double> weights;

        #endregion //-----------------------------------------//

        #region CONSTRUCTOR
        //----------------------------------------------------//

        /// <summary>
        /// Create new FCM relation
        /// </summary>
        /// <param name="concept">related concept</param>
        public RSimple(Concept concept)
        {
            inputType = "simple";
            procType = "linear";
            next = concept;
            previous = new List<Concept>();
        }

        #endregion //-----------------------------------------//

        #region METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Attach new preceding concept to the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        public void attach(Concept concept)
        {
            previous.Add(concept);
            weights.Add(1);
        }

        /// <summary>
        /// Detach existing preceding concept from the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        public void detach(Concept concept)
        {
            
        }

        /// <summary>
        /// Get string containing all relations weights (or single weight related to specified concept).
        /// </summary>
        /// <param name="concept">optionally specified preceding concept</param>
        /// <returns>string containing relation weights separated by semicolons</returns>
        public string get(Concept concept = null)
        {
            //return all weights if no concept is specified
            if (concept == null)
                return string.Join(";", weights);
            //return single weight if concept is specified and is attached to the relation
            for (int i = 0; i < previous.Count(); i++)
                if (String.Equals(concept.name, previous[i].name))
                    return weights[i].ToString();
            //return empty string if there is no such concept attached
            return "";
        }

        /// <summary>
        /// Set relation using string containing weights separated by semicolons (or single weight related to specified concept)
        /// </summary>
        /// <param name="formatString">connection information in string format</param>
        /// <param name="concept">optionally specified preceding concept</param>
        public void set(string formatString, Concept concept = null)
        {

        }

        /// <summary>
        /// Propagate inputs through relation and calculate new value for the following concept.
        /// </summary>
        public double propagate()
        {
            return 1;
        }

        #endregion //-----------------------------------------//
    }
}
