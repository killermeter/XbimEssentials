// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool Xbim.CodeGeneration 
//  
//     Changes to this file may cause incorrect behaviour and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Xbim.Ifc4.MeasureResource;
using System;
using System.Collections.Generic;
using Xbim.Common;
using Xbim.Common.Exceptions;

namespace Xbim.Ifc4.PropertyResource
{
	[ExpressType("IFCPROPERTYLISTVALUE", 855)]
	// ReSharper disable once PartialTypeWithSinglePart
	public  partial class @IfcPropertyListValue : IfcSimpleProperty, IInstantiableEntity, IEqualityComparer<@IfcPropertyListValue>, IEquatable<@IfcPropertyListValue>
	{
		//internal constructor makes sure that objects are not created outside of the model/ assembly controlled area
		internal IfcPropertyListValue(IModel model) : base(model) 		{ 
			Model = model; 
			_listValues = new OptionalItemSet<IfcValue>( this, 0 );
		}

		#region Explicit attribute fields
		private OptionalItemSet<IfcValue> _listValues;
		private IfcUnit _unit;
		#endregion
	
		#region Explicit attribute properties
		[EntityAttribute(3, EntityAttributeState.Optional, EntityAttributeType.List, EntityAttributeType.Class, 1, -1)]
		public OptionalItemSet<IfcValue> @ListValues 
		{ 
			get 
			{
				if(ActivationStatus != ActivationStatus.NotActivated) return _listValues;
				((IPersistEntity)this).Activate(false);
				return _listValues;
			} 
		}
	
		[EntityAttribute(4, EntityAttributeState.Optional, EntityAttributeType.Class, EntityAttributeType.None, -1, -1)]
		public IfcUnit @Unit 
		{ 
			get 
			{
				if(ActivationStatus != ActivationStatus.NotActivated) return _unit;
				((IPersistEntity)this).Activate(false);
				return _unit;
			} 
			set
			{
				SetValue( v =>  _unit = v, _unit, value,  "Unit");
			} 
		}
	
		#endregion



		#region IPersist implementation
		public  override void Parse(int propIndex, IPropertyValue value, int[] nestedIndex)
		{
			switch (propIndex)
			{
				case 0: 
				case 1: 
					base.Parse(propIndex, value, nestedIndex); 
					return;
				case 2: 
					if (_listValues == null) _listValues = new OptionalItemSet<IfcValue>( this );
					_listValues.InternalAdd((IfcValue)value.EntityVal);
					return;
				case 3: 
					_unit = (IfcUnit)(value.EntityVal);
					return;
				default:
					throw new XbimParserException(string.Format("Attribute index {0} is out of range for {1}", propIndex + 1, GetType().Name.ToUpper()));
			}
		}
		
		public  override string WhereRule() 
		{
            throw new System.NotImplementedException();
		/*WR31:               )) = 0;*/
		}
		#endregion

		#region Equality comparers and operators
        public bool Equals(@IfcPropertyListValue other)
	    {
	        return this == other;
	    }

	    public override bool Equals(object obj)
        {
            // Check for null
            if (obj == null) return false;

            // Check for type
            if (GetType() != obj.GetType()) return false;

            // Cast as @IfcPropertyListValue
            var root = (@IfcPropertyListValue)obj;
            return this == root;
        }
        public override int GetHashCode()
        {
            //good enough as most entities will be in collections of  only one model, equals distinguishes for model
            return EntityLabel.GetHashCode(); 
        }

        public static bool operator ==(@IfcPropertyListValue left, @IfcPropertyListValue right)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(left, right))
                return true;

            // If one is null, but not both, return false.
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return (left.EntityLabel == right.EntityLabel) && (left.Model == right.Model);

        }

        public static bool operator !=(@IfcPropertyListValue left, @IfcPropertyListValue right)
        {
            return !(left == right);
        }


        public bool Equals(@IfcPropertyListValue x, @IfcPropertyListValue y)
        {
            return x == y;
        }

        public int GetHashCode(@IfcPropertyListValue obj)
        {
            return obj == null ? -1 : obj.GetHashCode();
        }
        #endregion
	}
}