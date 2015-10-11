using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm
{
    /// <summary>
    /// Represents single FCM concept
    /// </summary>
    public class Concept
    {
        #region PROPERTIES & FIELDS
        //----------------------------------------------------//

        //concept name
        public string name;                 //unique name of the concept

        //concept values
        public double value;                //activation value of the concept in time "t"
        public double newValue;             //activation value of the concept in time "t+1"

        //backpropagation
        public double delta;                //concept activation error in time "t"
        public double newDelta;             //concept activation error in time "t+1"

        //relations
        internal IRelation relation;        //concept relations

        //membership functions
        internal MF inputMF;                //function used for fuzzification
        internal MF outputMF;               //function used for defuzzification

        #endregion //-----------------------------------------//

        #region CONSTRUCTOR
        //----------------------------------------------------//

        public Concept(string name)
        {
            //set name
            this.name = name;
            //initialize concept values
            this.newValue = 0;
            this.value = 0;
            //create relations
            this.relation = new RSimple();
            //set membership functions
            this.inputMF = new MF();
            this.outputMF = new MF();
        }

        #endregion //-----------------------------------------//
    }
}
