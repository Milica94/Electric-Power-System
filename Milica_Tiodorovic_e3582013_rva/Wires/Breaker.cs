///////////////////////////////////////////////////////////
//  Breaker.cs
//  Implementation of the Class Breaker
//  Generated by Enterprise Architect
//  Created on:      21-Sep-2016 12:15:11 PM
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using CIM.IEC61970.Base.Wires;
namespace CIM.IEC61970.Base.Wires {
	/// <summary>
	/// A mechanical switching device capable of making, carrying, and breaking
	/// currents under normal circuit conditions and also making, carrying for a
	/// specified time, and breaking currents under specified abnormal circuit
	/// conditions e.g.  those of short circuit.
	/// </summary>
	public class Breaker : ProtectedSwitch {

		/// <summary>
		/// The transition time from open to close.
		/// </summary>
		public float inTransitTime
        {get;
            set;
        }

        public string trafoId
        {
            get;
            set;
        }
		public Breaker(){

		}

		~Breaker(){

		}

	}//end Breaker

}//end namespace Wires