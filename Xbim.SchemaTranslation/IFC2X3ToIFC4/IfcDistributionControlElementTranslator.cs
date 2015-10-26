// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool XbimTranslatorGenerator
//  
//     Changes to this file may cause incorrect behaviour and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using Xbim.Common;
using System.Linq;

namespace Xbim.SchemaTranslation.IFC2X3ToIFC4
{
	public partial class IfcDistributionControlElementTranslator : ITranslator
	{
		private readonly int[] _translatesProperties = new int[]{8};
		
		public string OriginalSchema { get { return "IFC2X3";} }
		public string TranslatesEntity { get { return "IFCDISTRIBUTIONCONTROLELEMENT";} }
		public int[] TranslatesProperties { get { return _translatesProperties;} }

		//this function returns entity name in target schema
		public string TranslateEntity()
		{
			return "IFCDISTRIBUTIONCONTROLELEMENT";
		}

		//this function translates different data types, different order etc.
		public void Parse(int propIndex, IPropertyValue value, int[] nested, ParseDelegate parse)
		{
			if(!_translatesProperties.Contains(propIndex))
			{
				//just use delegate function if there is no spacial handling for this property
				parse(propIndex, value, nested);
				return;
			}
		}
	}
}
