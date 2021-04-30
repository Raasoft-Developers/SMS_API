Param(
    [parameter(Mandatory=$false)][string]$imageTag ="",
	[parameter(Mandatory=$false)][string]$num ="*"

    )
$rollout=@("nvgapisms","nvgsmsbackgroundtask")

if($num -eq "*"){
$contexts=@("dev","test","stag","prod");
$context=""

   for($i=0; $i -lt $contexts.Count ;$i++){
       $name=$contexts[$i];
	   echo "$i) $name"
   }
   $num = Read-Host "Select the number to enter the context  "
   for($i=0; $i -lt $contexts.Count ;$i++){
	   if($i -eq $num){
          $context=$contexts[$i];
		  kubectl config use-context tv2-${context}-etms
	    }
}
	For($i=0;$i -lt $rollout.Count;$i++){
       $name=$rollout[$i];
       echo "$i) [$name]";
}
echo ""
$num = read-host "Select number(s) seperated by space and enter * to select all  "
}
echo "INPUT: [$num]"
$indexes=$num.Split(",");
$indexes
$services=@()
For($i=0;$i -lt $rollout.Count;$i++){
	
	if($num -eq "*"){
	$services += $rollout[$i];
	}
	elseif( $i -in $indexes){
    $services += $rollout[$i];	
	}
}
if($imageTag  -eq ""){
$imageTag = read-host "enter the version"
}
if($imageTag  -eq ""){
$imageTag = "latest"
}
For($i=0;$i -lt $services.Count;$i++){
      $name = $services[$i];
	  $registryImage= "$name=registry.digitalocean.com/tv2/$name"
	  echo " $name $nameservice     "

      kubectl set image deployment/$name ${registryImage}:$imageTag --record

 }