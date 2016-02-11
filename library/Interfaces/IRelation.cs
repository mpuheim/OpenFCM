using System.Collections.Generic;

namespace libfcm.Interfaces
{
    /// <summary>
    /// Interface for MISO relation (multiple input, single output) between preceding concepts and single following concept.
    /// </summary>
    public interface IRelation
    {
        #region REQUIRED PROPERTIES
        //----------------------------------------------------//

        //connected previous concepts
        List<Concept> previous { get; set; }

        #endregion //-----------------------------------------//

        #region REQUIRED METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Return relation model information.
        /// </summary>
        /// <returns>string containing relation model information</returns>
        string info();
        
        /// <summary>
        /// Attach new preceding concept to the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        void attach(Concept concept);

        /// <summary>
        /// Detach existing preceding concept from the relation.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        void detach(Concept concept);

        /// <summary>
        /// Get information about relation (optionaly specified by provided parameters).
        /// </summary>
        /// <param name="parameters">optional function parameters</param>
        /// <returns>relation information in string format</returns>
        string get(params string[] parameters);

        /// <summary>
        /// Set relation using provided data
        /// </summary>
        /// <param name="parameters">optional function parameters</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        int set(params string[] parameters);

        /// <summary>
        /// Propagate inputs through relation and calculate new value for the following concept.
        /// </summary>
        double propagate();

        /// <summary>
        /// Error delta backpropagation
        /// </summary>
        /// <param name="delta">error delta</param>
        void backprop(double delta);

        /// <summary>
        /// Relation adaptation/learning via the "delta rule"
        /// </summary>
        /// <param name="delta">error delta</param>
        /// <param name="delta">learning rate</param>
        void adapt(double delta, double gama);

        #endregion //-----------------------------------------//
    }
}
