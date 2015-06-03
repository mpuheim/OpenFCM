using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm
{
    #region MATLAB INFO
    /*
        - Load in Matlab using:
        NET.addAssembly('E:\Data\Programming\OpenFCM\library\bin\Debug\libfcm.dll')
        
        - Create map using:
        map = libfcm.FCM
    */
    #endregion //INFO

    /// <summary>
    /// Represents fuzzy cognitive map. Provides functions to add, connect and configure map concepts and functions to calculate map updates.
    /// </summary>
    public class FCM
    {
        #region PROPERTIES & FIELDS
        //----------------------------------------------------//
        
        private List<Concept> concepts; //list of concepts
        private Config config;          //current FCM configuration

        #endregion //-----------------------------------------//

        #region CONSTRUCTOR
        //----------------------------------------------------//

        /// <summary>
        /// Create new fuzzy cognitive map
        /// </summary>
        public FCM()
        {
            concepts = new List<Concept>();
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - ADD/REMOVE
        //----------------------------------------------------//

        /// <summary>
        /// Add new concept to the FCM
        /// </summary>
        /// <param name="name">unique name</param>
        public void add(string name)
        {
            //TODO
        }

        /// <summary>
        /// Remove concept from FCM
        /// </summary>
        /// <param name="name">name</param>
        public void remove(string name)
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - CONNECT/DISCONNECT
        //----------------------------------------------------//

        /// <summary>
        /// Connect two concepts
        /// </summary>
        /// <param name="from">name of the preceding concept</param>
        /// <param name="name">name of the following concept</param>
        public void connect(string from, string to)
        {
            //TODO
        }

        /// <summary>
        /// Disonnect two concepts
        /// </summary>
        /// <param name="from">name of the preceding concept</param>
        /// <param name="name">name of the following concept</param>
        public void disconnect(string from, string to)
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - GET/SET VALUE
        //----------------------------------------------------//

        /// <summary>
        /// Get current activation value of the concept
        /// </summary>
        /// <param name="name">concept name</param>
        /// <returns>concept activation value</returns>
        public double get(string name)
        {
            //TODO
            return 1.0;
        }

        /// <summary>
        /// Set current activation value of the concept
        /// </summary>
        /// <param name="name">concept name</param>
        /// <param name="value">new activation value of concept</param>
        public void set(string name, double value)
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - CONFIGURE RELATIONS
        //----------------------------------------------------//

        /// <summary>
        /// Configuration of concept relations
        /// </summary>
        /// <param name="param">param</param>
        public void relation(string param)
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - MF CONFIGURATION
        //----------------------------------------------------//

        /// <summary>
        /// Configuration of concept membership functions
        /// </summary>
        /// <param name="param">param</param>
        public void mf(string param)
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region MAP UPDATE
        //----------------------------------------------------//

        /// <summary>
        /// Update activation values of all concept within the map
        /// </summary>
        public void update()
        {
            //TODO
        }

        #endregion //-----------------------------------------//

        #region LIST CONCEPTS
        //----------------------------------------------------//

        /// <summary>
        /// Return string containing names of all concepts within the map
        /// </summary>
        /// <returns>string containing names of all concepts separated by semicolons</returns>
        public string list()
        {
            //TODO
            return "concepts";
        }

        /// <summary>
        /// Return string containing names of all concepts preceding single concept specified by name
        /// </summary>
        /// <param name="name">concept name</param>
        /// <returns>string containing names of all preceding concepts separated by semicolons</returns>
        public string listPreceding(string name)
        {
            //TODO
            return "concepts";
        }

        #endregion //-----------------------------------------//
        
        #region METHOD TEMPLATE
        //----------------------------------------------------//

        /// <summary>
        /// Sample method template
        /// </summary>
        /// <param name="first">first param</param>
        /// <param name="second">second param</param>
        /// <returns>value</returns>
        public int method(int first, int second)
        {
            return first + second;
        }

        #endregion //-----------------------------------------//
    }

}
