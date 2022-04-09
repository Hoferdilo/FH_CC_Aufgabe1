#Variables
$rgName = "RGCCAufgabe1"
$gitRepo = "https://github.com/Hoferdilo/FH_CC_Aufgabe1"
$nameOfWebService = 'HoferAufgabe1-Service'
$loc = "West Europe"
Connect-AzAccount
$rg = New-AzResourceGroup -Name $rgName -Location $loc
$plan = New-AzAppServicePlan -Name "$nameOfWebService-plan" -Location $loc -ResourceGroupName $rg.ResourceGroupName -Tier Free
$app = New-AzWebApp -Name $nameOfWebService -Location $loc -AppServicePlan $plan.Name -ResourceGroupName $rg.ResourceGroupName
$PropertiesObject = @{
    repoUrl = "$gitrepo";
    branch = "master";
    isManualIntegration = "true";
}
Set-AzResource -Properties $PropertiesObject -ResourceGroupName $rg.ResourceGroupName -ResourceType Microsoft.Web/sites/sourcecontrols -ResourceName $nameOfWebService/Web -ApiVersion 2015-08-01 -Force