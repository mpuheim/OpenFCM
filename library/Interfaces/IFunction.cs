using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libfcm.Interfaces
{
    /// <summary>
    /// Interface for standard SISO function (single input, single output) in form out=f(in).
    /// </summary>
    public interface IFunction
    {
        #region REQUIRED METHODS
        //----------------------------------------------------//

        /// <summary>
        /// Return basic information about function.
        /// </summary>
        /// <returns> string containing basic information about function</returns>
        string info();

        /// <summary>
        /// Return detailed information about function (aka serialization).
        /// </summary>
        /// <returns> string containing serialized function object</returns>
        string get();

        /// <summary>
        /// Specify function via predefined set of parameters (aka deserialization).
        /// </summary>
        /// <param name="parameters">Parameters or formula specifiing the function</param>
        /// <returns>0 if successfull, -1 otherwise</returns>
        int set(params string[] parameters);

        /// <summary>
        /// Get function derivative.
        /// </summary>
        /// <returns>either IFunction object or null</returns>
        IFunction getDerivative();

        /// <summary>
        /// Get inverse function.
        /// </summary>
        /// <returns>either IFunction object or null</returns>
        IFunction getInverse();

        /// <summary>
        /// Calculate function output as out=f(in).
        /// </summary>
        /// <param name="parameters">input</param>
        /// <returns>output</returns>
        double evaluate(double input);

        #endregion //-----------------------------------------//
    }
}
