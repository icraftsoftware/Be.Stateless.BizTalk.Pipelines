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

using System.Collections.Generic;
using Be.Stateless.BizTalk.Schema;
using Be.Stateless.BizTalk.Schemas.Xml;

namespace Be.Stateless.BizTalk.Data
{
	public class EnvelopeGenerator
	{
		public static IEnumerable<object[]> InvalidEnvelopes
		{
			get
			{
				yield return new object[] {
					$"<s0:{nameof(Envelope)} xmlns:s0='{SchemaMetadata.For<Envelope>().TargetNamespace}' />"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelTwo)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}' />"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelTwo)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}' />"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"</s0:{nameof(Envelopes.LevelOne)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.LevelTwo)} />"
					+ $"</s0:{nameof(Envelopes.LevelOne)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"</s0:{nameof(Envelopes.LevelOne)}>"
				};
			}
		}

		public static IEnumerable<object[]> ValidEnvelopes
		{
			get
			{
				yield return new object[] {
					$"<s0:{nameof(Envelope)} xmlns:s0='{SchemaMetadata.For<Envelope>().TargetNamespace}'>"
					+ $"</s0:{nameof(Envelope)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelTwo)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.Part)} />"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelTwo)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.Part)}></s0:{nameof(Envelopes.Part)}>"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"<s0:{nameof(Envelopes.Part)} />"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"</s0:{nameof(Envelopes.LevelOne)}>"
				};

				yield return new object[] {
					$"<s0:{nameof(Envelopes.LevelOne)} xmlns:s0='{SchemaMetadata.For<Envelopes>().TargetNamespace}'>"
					+ $"<s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"<s0:{nameof(Envelopes.Part)}></s0:{nameof(Envelopes.Part)}>"
					+ $"</s0:{nameof(Envelopes.LevelTwo)}>"
					+ $"</s0:{nameof(Envelopes.LevelOne)}>"
				};
			}
		}
	}
}
