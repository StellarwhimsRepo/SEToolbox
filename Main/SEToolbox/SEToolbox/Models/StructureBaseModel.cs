﻿namespace SEToolbox.Models
{
    using Sandbox.Common.ObjectBuilders;
    using Sandbox.Common.ObjectBuilders.Voxels;
    using SEToolbox.Interfaces;
    using SEToolbox.Interop;
    using SEToolbox.Support;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class StructureBaseModel : BaseModel, IStructureBase
    {
        #region fields

        // Fields are marked as NonSerialized, as they aren't required during the drag-drop operation.

        [NonSerialized]
        private MyObjectBuilder_EntityBase _entityBase;

        [NonSerialized]
        private ClassType _classType;

        [NonSerialized]
        private string _name;

        [NonSerialized]
        private string _description;

        [NonSerialized]
        private double _playerDistance;

        [NonSerialized]
        private bool _isBusy;

        private string _serializedEntity;

        #endregion

        #region ctor

        public StructureBaseModel()
        {
        }

        public StructureBaseModel(MyObjectBuilder_EntityBase entityBase)
        {
            this.EntityBase = entityBase;
        }

        #endregion

        #region Properties

        [XmlIgnore]
        public virtual MyObjectBuilder_EntityBase EntityBase
        {
            get
            {
                return this._entityBase;
            }
            set
            {
                if (value != this._entityBase)
                {
                    this._entityBase = value;
                    this.UpdateGeneralFromEntityBase();
                    this.RaisePropertyChanged(() => EntityBase);
                }
            }
        }

        [XmlIgnore]
        public long EntityId
        {
            get
            {
                return this._entityBase.EntityId;
            }

            set
            {
                if (value != this._entityBase.EntityId)
                {
                    this._entityBase.EntityId = value;
                    this.RaisePropertyChanged(() => EntityId);
                }
            }
        }

        [XmlIgnore]
        public MyPositionAndOrientation? PositionAndOrientation
        {
            get
            {
                return this._entityBase.PositionAndOrientation;
            }

            set
            {
                if (!EqualityComparer<MyPositionAndOrientation?>.Default.Equals(value, this._entityBase.PositionAndOrientation))
                //if (value != this.entityBase.PositionAndOrientation)
                {
                    this._entityBase.PositionAndOrientation = value;
                    this.RaisePropertyChanged(() => PositionAndOrientation);
                }
            }
        }

        [XmlIgnore]
        public double PositionX
        {
            get
            {
                return this._entityBase.PositionAndOrientation.Value.Position.X.ToDouble();
            }

            set
            {
                if ((float)value != this._entityBase.PositionAndOrientation.Value.Position.X)
                {
                    var pos = this._entityBase.PositionAndOrientation.Value;
                    pos.Position.X = (float)value;
                    this._entityBase.PositionAndOrientation = pos;
                    this.RaisePropertyChanged(() => PositionX);
                }
            }
        }

        [XmlIgnore]
        public double PositionY
        {
            get
            {
                return this._entityBase.PositionAndOrientation.Value.Position.Y.ToDouble();
            }

            set
            {
                if ((float)value != this._entityBase.PositionAndOrientation.Value.Position.Y)
                {
                    var pos = this._entityBase.PositionAndOrientation.Value;
                    pos.Position.Y = (float)value;
                    this._entityBase.PositionAndOrientation = pos;
                    this.RaisePropertyChanged(() => PositionY);
                }
            }
        }

        [XmlIgnore]
        public double PositionZ
        {
            get
            {
                return this._entityBase.PositionAndOrientation.Value.Position.Z.ToDouble();
            }

            set
            {
                if ((float)value != this._entityBase.PositionAndOrientation.Value.Position.Z)
                {
                    var pos = this._entityBase.PositionAndOrientation.Value;
                    pos.Position.Z = (float)value;
                    this._entityBase.PositionAndOrientation = pos;
                    this.RaisePropertyChanged(() => PositionZ);
                }
            }
        }

        [XmlIgnore]
        public ClassType ClassType
        {
            get
            {
                return this._classType;
            }

            set
            {
                if (value != this._classType)
                {
                    this._classType = value;
                    this.RaisePropertyChanged(() => ClassType);
                }
            }
        }

        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                return this._name;
            }

            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.RaisePropertyChanged(() => DisplayName);
                }
            }
        }

        [XmlIgnore]
        public string Description
        {
            get
            {
                return this._description;
            }

            set
            {
                if (value != this._description)
                {
                    this._description = value;
                    this.RaisePropertyChanged(() => Description);
                }
            }
        }

        [XmlIgnore]
        public double PlayerDistance
        {
            get
            {
                return this._playerDistance;
            }

            set
            {
                if (value != this._playerDistance)
                {
                    this._playerDistance = value;
                    this.RaisePropertyChanged(() => PlayerDistance);
                }
            }
        }

        public string SerializedEntity
        {
            get
            {
                return this._serializedEntity;
            }

            set
            {
                if (value != this._serializedEntity)
                {
                    this._serializedEntity = value;
                    this.RaisePropertyChanged(() => SerializedEntity);
                }
            }
        }

        [XmlIgnore]
        public bool IsBusy
        {
            get { return this._isBusy; }

            set
            {
                if (value != this._isBusy)
                {
                    this._isBusy = value;
                    this.RaisePropertyChanged(() => IsBusy);
                    if (this._isBusy)
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
        }

        #endregion

        #region methods

        public virtual void UpdateGeneralFromEntityBase()
        {
            this.ClassType = ClassType.Unknown;
        }

        public static IStructureBase Create(MyObjectBuilder_EntityBase entityBase, string savefilePath)
        {
            if (entityBase is MyObjectBuilder_VoxelMap)
            {
                return new StructureVoxelModel(entityBase, savefilePath);
            }
            else if (entityBase is MyObjectBuilder_Character)
            {
                return new StructureCharacterModel(entityBase);
            }
            else if (entityBase is MyObjectBuilder_CubeGrid)
            {
                return new StructureCubeGridModel(entityBase);
            }
            else if (entityBase is MyObjectBuilder_FloatingObject)
            {
                return new StructureFloatingObjectModel(entityBase);
            }
            else if (entityBase is MyObjectBuilder_Meteor)
            {
                return new StructureMeteorModel(entityBase);
            }
            else
            {
                return new StructureUnknownModel(entityBase);
                //throw new NotImplementedException(string.Format("A new object has not been catered for in the StructureBase, of type '{0}'.", entityBase.GetType()));
            }
        }

        public virtual void InitializeAsync()
        {
            // to be overridden.
        }

        #endregion
    }
}
