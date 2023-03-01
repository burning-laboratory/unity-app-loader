<p>
      <img src="https://i.ibb.co/KbMtdsw/Git-Hub-Logo.png" alt="Project Logo" width="726">
</p>

<p>
    <img src="https://build.burning-lab.com/app/rest/builds/buildType:id:UnityAssets_ComBurningLabApploader_DevelopmentBuild/statusIcon.svg" alt="Build Status">
    <a href="https://burning-lab.youtrack.cloud/agiles/131-18/current"><img src="https://img.shields.io/badge/Roadmap-YouTrack-orange" alt="Roadmap Link"></a>
    <img src="https://img.shields.io/badge/Engine-{unity_version}-blueviolet" alt="Unity Version">
    <img src="https://img.shields.io/badge/Version-{package_version}-blue" alt="Game Version">
    <img src="https://img.shields.io/badge/License-MIT-success" alt="License">
</p>

## About

Smart application loading pipeline controller. With this package, you can create and manage your own application loading stages.

## Installation

1. Add Burning-Lab registry to Unity Project.
2. Add Open UPM Registry to Unity Project for importing external dependencies. 
3. Install **App Loader** package via Unity Package Manager.

**Burning-Lab Registry:**

```json
    {
      "name": "Burning-Lab Registry",
      "url": "https://packages.burning-lab.com",
      "scopes": [
        "com.burning-lab"
      ]
    }
```

**Open UPM Registry:**

```json
    {
      "name": "Open UPM Registry",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.mackysoft.serializereference-extensions"
      ]
    }
```

### Included stages

Stages list included in package.

- Game Objects stages:
  - `InstantiateLocalGameObjectActionPipelineStage`: Instantiate Game Object Stage - Instantiate local game object.
  - `DestroyLocalGameObjectActionPipelineStage`: Destroy Local Game Object Stage - Destroy local game object.

- Scenes stages:
  - `LoadLocalSceneActionPipelineStage`: Load Local Scene Stage - Loading local scene.
  - `UnloadLocalSceneActionPipelineStage`: Unload Local Scene Stage - Unloading local scene.
  - `MoveGameObjectToSceneActionPipelineStage`: Move Game Object To Scene Stage - Moving game object to scene.
  - `MarkSceneAsActiveActionPipelineStage`: Mark Scene As Active Stage - Mark scene as active.

- Misc stages:
  - `WaitAnyKeyDownActionPipelineStage`: Wait Any Key Down Stage - Wait any key pressing or screen touch stage.


