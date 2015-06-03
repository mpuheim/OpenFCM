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
        public double newValue;             //value of the concept in time "t+1"
        public double currentValue;         //value of the concept in time "t"

        //relations
        internal IRelation relation;         //concept relations

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
            this.currentValue = 0;
            //create relations
            this.relation = new RSimple(this);
            //set membership functions
            this.inputMF = new MF();
            this.outputMF = new MF();
        }

        #endregion //-----------------------------------------//
    }
}
