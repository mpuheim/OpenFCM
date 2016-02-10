using System.Collections.Generic;
using System.Linq;

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

        private Concept c1;             //helper variable
        private Concept c2;             //helper variable

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
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int add(string name)
        {
            if (string.IsNullOrEmpty(name))
                return -1;
            if (concepts.Exists(c => c.name == name))
                return -1;
            concepts.Add(new Concept(name));
            return 0;
        }

        /// <summary>
        /// Remove concept from the FCM
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>0 if concept is removed, -1 otherwise</returns>
        public int remove(string name)
        {
            if (string.IsNullOrEmpty(name))
                return -1;
            if (concepts.Exists(c => c.name == name) == false)
                return 0;
            int removed = concepts.RemoveAll(c => c.name == name);
            if (removed == 0)
                return -1;
            return 0;
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - CONNECT/DISCONNECT
        //----------------------------------------------------//

        /// <summary>
        /// Connect two concepts
        /// </summary>
        /// <param name="from">name of the preceding concept</param>
        /// <param name="name">name of the following concept</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int connect(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return -1;
            c1 = concepts.FirstOrDefault(c => c.name == from);
            c2 = concepts.FirstOrDefault(c => c.name == to);
            if (c1 == null || c2 == null)
                return -1;
            if (c2.relation.previous.Contains(c1))
                return -1;
            c2.relation.attach(c1);
            return 0;
        }

        /// <summary>
        /// Disonnect two concepts
        /// </summary>
        /// <param name="from">name of the preceding concept</param>
        /// <param name="name">name of the following concept</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int disconnect(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return -1;
            c1 = concepts.FirstOrDefault(c => c.name == from);
            c2 = concepts.FirstOrDefault(c => c.name == to);
            if (c1 == null || c2 == null)
                return -1;
            if (c2.relation.previous.Contains(c1) == false)
                return -1;
            c2.relation.detach(c1);
            return 0;
        }

        #endregion //-----------------------------------------//

        #region CONCEPTS - GET/SET VALUE
        //----------------------------------------------------//

        /// <summary>
        /// Get current activation value of the concept
        /// </summary>
        /// <param name="name">concept name</param>
        /// <returns>concept activation value or NaN if no such concept exists</returns>
        public double get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return double.NaN;
            c1 = concepts.FirstOrDefault(c => c.name == name);
            if (c1 == null)
                return double.NaN;
            return c1.value;
        }

        /// <summary>
        /// Set current activation value of the concept
        /// </summary>
        /// <param name="name">concept name</param>
        /// <param name="value">new activation value of concept</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int set(string name, double value)
        {
            if (string.IsNullOrEmpty(name))
                return -1;
            c1 = concepts.FirstOrDefault(c => c.name == name);
            if (c1 == null)
                return -1;
            c1.value = value;
            return 0;
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

        #region RELATIONS - GET/SET VALUE
        //----------------------------------------------------//

        /// <summary>
        /// Get concept relation
        /// </summary>
        /// <param name="name">name of the following concept</param>
        /// <param name="param">optional relation query information (such as specific preceding concept name)</param>
        /// <returns>specified relation information or error message</returns>
        public string getRelation(string name, params string[] parameters)
        {
            if (string.IsNullOrEmpty(name))
                return "error - wrong concept name";
            c1 = concepts.FirstOrDefault(c => c.name == name);
            if (c1 == null)
                return "error - no such concept";
            return c1.relation.get(parameters);
        }

        /// <summary>
        /// Set concept relation
        /// </summary>
        /// <param name="name">name of the following concept</param>
        /// <param name="param">optional relation query information (e.g.: preceding concept name, weight value)</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        public int setRelation(string name, params string[] parameters)
        {
            if (string.IsNullOrEmpty(name))
                return -1;
            c1 = concepts.FirstOrDefault(c => c.name == name);
            if (c1 == null)
                return -1;
            return c1.relation.set(parameters);
        }

        #endregion //-----------------------------------------//

        #region MAP UPDATE
        //----------------------------------------------------//

        /// <summary>
        /// Update activation values of all concept within the map
        /// </summary>
        public void update()
        {
            foreach (Concept c in concepts)
                c.newValue = c.relation.propagate();
            foreach (Concept c in concepts)
                c.value = c.newValue;
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
            //return empty string if there are no concepts
            if (concepts.Count == 0)
                return "";
            //return list of concepts
            List<string> toReturn = new List<string>(concepts.Count());
            foreach (Concept c in concepts)
                    toReturn.Add(c.name);
            return string.Join(";", toReturn);
        }

        /// <summary>
        /// Return string containing names of all concepts preceding single concept specified by name
        /// </summary>
        /// <param name="name">concept name</param>
        /// <returns>string containing names of all preceding concepts separated by semicolons</returns>
        public string listPreceding(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "error - wrong concept name";
            c1 = concepts.FirstOrDefault(c => c.name == name);
            if (c1 == null)
                return "error - no such concept";
            if (c1.relation.previous.Count == 0)
                return "";
            List<string> toReturn = new List<string>(c1.relation.previous.Count());
            foreach (Concept c in c1.relation.previous)
                toReturn.Add(c.name);
            return string.Join(";", toReturn);
        }

        #endregion //-----------------------------------------//
        
        #region METHOD TEMPLATE
        //----------------------------------------------------//

        /// <summary>
        /// Sample method template - NOT USED
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
