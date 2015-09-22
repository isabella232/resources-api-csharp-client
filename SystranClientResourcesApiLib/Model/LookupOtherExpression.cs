using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Systran.ResourcesClientLib.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class LookupOtherExpression {
    
    
    [DataMember(Name="context", EmitDefaultValue=false)]
    public string Context { get; set; }

    
    
    [DataMember(Name="source", EmitDefaultValue=false)]
    public string Source { get; set; }

    
    
    [DataMember(Name="target", EmitDefaultValue=false)]
    public string Target { get; set; }

    

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class LookupOtherExpression {\n");
      
      sb.Append("  Context: ").Append(Context).Append("\n");
      
      sb.Append("  Source: ").Append(Source).Append("\n");
      
      sb.Append("  Target: ").Append(Target).Append("\n");
      
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
  
  
}