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
using Be.Stateless.BizTalk.Component;
using Be.Stateless.BizTalk.Dsl.Pipeline;
using Be.Stateless.BizTalk.Schema;
using Microsoft.BizTalk.Component;
using Microsoft.XLANGs.BaseTypes;

namespace Be.Stateless.BizTalk.MicroPipelines
{
	[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "DSL-based pipeline definition.")]
	public class FFReceive : ReceivePipeline
	{
		[SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose will be called by BizTalk pipeline execution engine.")]
		public FFReceive()
		{
			Description = "Flat-File receive micro-pipeline.";
			Version = new(1, 0);
			Stages.Decode
				.AddComponent(new FailedMessageRoutingEnablerComponent())
				.AddComponent(new MicroPipelineComponent { Enabled = true });
			Stages.Disassemble
				.AddComponent(
					new FFDasmComp {
						DocumentSpecName = new(SchemaMetadata.For<Any>().DocumentSpec.DocSpecStrongName),
						ValidateDocumentStructure = true
					});
			Stages.Validate
				.AddComponent(new MicroPipelineComponent { Enabled = true });
		}
	}
}
