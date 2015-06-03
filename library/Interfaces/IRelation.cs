using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm
{
    /// <summary>
    /// Represents MISO relations between preceding concepts and single following concept. (MISO - multiple input, single output)
    /// </summary>
    internal interface IRelation
    {
        #region REQUIRED PROPERTIES
        //----------------------------------------------------//

        //capabilities of relation
        bool supportsBackprop { get; set; }       //can use error backpropagation learning.
        bool supportsSerialization { get; set; }  //can serialize and present model parameters (e.g. for EA learning).
        bool supportsIncrementality { get; set; } //can add/remove concepts without change to the current model.

        //connected preceding concepts
        List<Concept> previous { get; set; }

        #endregion //-----------------------------------------//

        #region REQUIRED METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Return relation model information.
        /// </summary>
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
        /// Get information about relation.
        /// </summary>
        /// <returns>relation information in string format</returns>
        string get();

        /// <summary>
        /// Get information about relation specific to provided preceding concept.
        /// </summary>
        /// <param name="concept">specified preceding concept</param>
        /// <returns>relation information in string format</returns>
        string get(Concept concept);

        /// <summary>
        /// Set relation using provided data
        /// </summary>
        /// <param name="formatString">relation information in string format</param>
        void set(string formatString);

        /// <summary>
        /// Propagate inputs through relation and calculate new value for the following concept.
        /// </summary>
        double propagate();

        #endregion //-----------------------------------------//
    }
}
