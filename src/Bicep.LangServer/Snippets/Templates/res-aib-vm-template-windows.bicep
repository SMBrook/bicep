// Azure Image Builder Windows VM Template Creation
resource AIBVMTemplateWin 'Microsoft.VirtualMachineImages/imageTemplates@2020-02-14' = {
  name: ${1:'name'}
  location: ${2:'location'}
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities:{
      ${resourceId(Microsoft.ManagedIdentity/userAssignedIdentities,${3:'REQUIRED'}}: {}
    }
  }
  properties: {
    buildTimeoutInMinutes: ${4:'buildTimeoutInMinutes'}
    vmProfile: {
        vmSize: ${5:'Standard_D2_v2'}
        }
  }
  source: {
    type: '${6|PlatformImage,ManagedImage,SharedImageVersion|}'
  }
  customize: [ 
    {
      type: 'WindowsRestart'
      restartCommand: 'shutdown /r /f /t 0'
      restartCheckCommand: 'echo Azure-Image-Builder-Restarted-the-VM  > c:\\buildArtifacts\\azureImageBuilderRestart.txt'
      restartTimeout: '5m'
    }
    {
      type: 'WindowsUpdate'
      searchCriteria: 'IsInstalled=0'
      filters: [
        'exclude:$_.Title -like \'*Preview*\''
        'include:$true'
      ]
      updateLimit: 40
    }
  ]
  distribute: [
    {
      type: '${7|managedImage,sharedImage,VHD|}'
      runOutputName: ${8:'runOutputname'}
    }  
  ]
}

