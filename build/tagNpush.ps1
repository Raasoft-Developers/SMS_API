Param(
    [parameter(Mandatory=$false)][string]$registry,
	[parameter(Mandatory=$false)][string]$chartsToDeploy="*",
	[parameter(Mandatory=$false)][string]$tag="",
	[parameter(Mandatory=$false)][string]$num="*"
)
if ([String]::IsNullOrEmpty($registry)) {
    Write-Host "Registry must be set to docker registry to use" -ForegroundColor Green
	$registry="registry.digitalocean.com/tv2"
    # exit 1 
}
if($tag -eq ""){
    $tag=git log -1 --pretty=format:%h;
}
if($tag -eq ""){
    $tag="latest";
}

#select services to push
$allservices = @("nvgapisms","nvgsmsbackgroundtask")
if($num -eq "*"){
For($i=0;$i -lt $allservices.Count;$i++){
	$name=$allservices[$i];
	
	echo "$i) [$name] " 
}
echo ""
$num = read-host "Select number(s) seperated by space and enter * to select all  "
}
echo "INPUT: [$num]"
$indexes=$num.Split(",");
$indexes
$services=@();


For($i=0;$i -lt $allservices.Count;$i++){
	if($num -eq ""){
	$services += $allservices[$i];
	}
	elseif( $i -in $indexes){
    $services += $allservices[$i];	
	}
}
#ready to tag and push

foreach ($svc in $services) {
    # Write-Host "Creating manifest for $svc and tags :latest, :master, and :dev"
    # docker manifest create $registry/${svc}:master $registry/${svc}:linux-master $registry/${svc}:win-master
    # docker manifest create $registry/${svc}:dev $registry/${svc}:linux-dev $registry/${svc}:win-dev
    # docker manifest create $registry/${svc}:latest $registry/${svc}:linux-latest $registry/${svc}:win-latest
    # Write-Host "Pushing manifest for $svc and tags :latest, :master, and :dev"
    # docker manifest push $registry/${svc}:latest
    # docker manifest push $registry/${svc}:dev
    # docker manifest push $registry/${svc}:master
	if ($chartsToDeploy -eq "*" -or $chartsToDeploy.Contains($svc) ) {
		docker tag ${svc} $registry/${svc}:$tag
		docker push "$registry/${svc}:$tag"
	}
	# exit 1
}
echo " The version number is:[$tag]"