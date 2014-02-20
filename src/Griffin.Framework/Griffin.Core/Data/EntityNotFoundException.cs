﻿using System;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace Griffin.Data
{
    /// <summary>
    ///     Thrown when a method which expects to find an entity doesn't.
    /// </summary>
    /// <remarks>
    /// <para>this error message will always include information to be able to identify the missing entity.</para>
    /// </remarks>
    [Serializable]
    public class EntityNotFoundException : DataException
    {
        private readonly string _commandText;
        private readonly string _parameters;

        public EntityNotFoundException(string message, IDbCommand command)
            : base(message)
        {
            if (command == null) throw new ArgumentNullException("command");

            _commandText = command.CommandText;
            _parameters = string.Join(", ",
                command.Parameters.Cast<IDataParameter>()
                    .Select(x => x.ParameterName + "=" + x.Value));
        }

        public EntityNotFoundException(string description, IDbCommand command, Exception inner)
            : base(description, inner)
        {
            if (description == null) throw new ArgumentNullException("description");
            if (inner == null) throw new ArgumentNullException("inner");

            _commandText = command.CommandText;
            _parameters = string.Join(", ",
                command.Parameters.Cast<IDataParameter>()
                    .Select(x => x.ParameterName + "=" + x.Value));
        }

        public EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _commandText = info.GetString("_commandText");
            _parameters = info.GetString("_parameters");
        }


        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <returns>
        ///     The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        public override string Message
        {
            get
            {
                return string.Format("{0}\r\nCommand: {1}\r\nParameters: {2}", base.Message, _commandText, _parameters);
            }
        }

        /// <summary>
        /// Gets command that was executed
        /// </summary>
        public string CommandText { get { return _commandText; }}


        /// <summary>
        /// The command parameter collection joined as a string
        /// </summary>
        public string CommandParameters { get { return _parameters; } }


        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param><param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param><exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_commandText", _commandText);
            info.AddValue("_parameters", _parameters);
        }
    }
}