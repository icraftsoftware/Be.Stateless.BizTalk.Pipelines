#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Be.Stateless.Resources;
using Microsoft.XLANGs.BaseTypes;

namespace Be.Stateless.BizTalk.Schemas.Xml
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	[SchemaType(SchemaTypeEnum.Document)]
	[SchemaRoots(new[] { nameof(LevelOne), nameof(LevelTwo), nameof(Part) })]
	public sealed class Envelopes : SchemaBase
	{
		#region Nested Type: LevelOne

		[Schema(NS_URI, nameof(LevelOne))]
		[BodyXPath(BODY_XPATH)]
		[SchemaRoots(new[] { nameof(LevelOne) })]
		public sealed class LevelOne : SchemaBase
		{
			#region Base Class Member Overrides

			protected override object RawSchema { get; set; }

			public override string[] RootNodes => new[] { nameof(LevelOne) };

			public override string XmlContent => _schema;

			#endregion

			private const string BODY_XPATH = "/*[local-name()='" + nameof(LevelOne) + "' and namespace-uri()='" + NS_URI + "']"
				+ "/*[local-name()='" + nameof(LevelTwo) + "' and namespace-uri()='" + NS_URI + "']"
				+ "/*[local-name()='" + nameof(Part) + "' and namespace-uri()='" + NS_URI + "']";
		}

		#endregion

		#region Nested Type: LevelTwo

		[Schema(NS_URI, nameof(LevelTwo))]
		[BodyXPath(BODY_XPATH)]
		[SchemaRoots(new[] { nameof(LevelTwo) })]
		public sealed class LevelTwo : SchemaBase
		{
			#region Base Class Member Overrides

			protected override object RawSchema { get; set; }

			public override string[] RootNodes => new[] { nameof(LevelTwo) };

			public override string XmlContent => _schema;

			#endregion

			private const string BODY_XPATH = "/*[local-name()='" + nameof(LevelTwo) + "' and namespace-uri()='" + NS_URI + "']"
				+ "/*[local-name()='" + nameof(Part) + "' and namespace-uri()='" + NS_URI + "']";
		}

		#endregion

		#region Nested Type: Part

		[Schema(NS_URI, nameof(Part))]
		[SchemaRoots(new[] { nameof(Part) })]
		public sealed class Part : SchemaBase
		{
			#region Base Class Member Overrides

			protected override object RawSchema { get; set; }

			public override string[] RootNodes => new[] { nameof(Part) };

			public override string XmlContent => _schema;

			#endregion
		}

		#endregion

		#region Base Class Member Overrides

		protected override object RawSchema { get; set; }

		public override string[] RootNodes => new[] { nameof(LevelOne), nameof(LevelTwo), nameof(Part) };

		public override string XmlContent => _schema;

		#endregion

		private const string NS_URI = "urn:schemas.stateless.be:biztalk:envelope:dummy:2021:07";

		private static readonly string _schema = ResourceManager.Load(
			Assembly.GetExecutingAssembly(),
			"Be.Stateless.BizTalk.Schemas.Xml.Envelopes.xsd",
			s => new StreamReader(s).ReadToEnd());
	}
}
