# App Loader

The component responsible for managing the application loading process.

### Settings:

- **-** **`Loading Pipeline (ActionPipeline)`** - Application loading pipeline. Contains a list of steps that must be performed to load the application. For more information, see the documentation with the [Actions Pipeline Engine](https://github.com/burning-laboratory/unity-actions-pipeline) package.

- **-** **`Dont Destroy On Load (bool)`** - Enabling this option to mark the game object with the component as DontDestroyOnLoad. The game object with the application loading component will not be destroyed during transitions between scenes.

- **-** **`Destroy After Loading (bool)`** - Enabling this option will delete the game object with the application loading component after the loading process is completed.

### Public Properties:

- **-** **`Loading Progress (float)`** - Application loading progress value.

### Events:

- **-** **`On App Loading Pipeline Start (Action)`** - The event of the start of the application loading process.

- **-** **`On App Loading Pipeline Stage Begin (Action<ActionPipelineStage>)`** - The event of the start of the application loading stage.

- **-** **`On App Loading Pipeline Stage End (Action<ActionPipelineStage>)`** - The event of the end of the application loading stage.

- **-** **`On App Loading Pipeline Complete (Action<ApplicationLoadingReport>)`** - The event of the end of the application loading process.

### Developer contacts:

**Email - [n.fridman@burning-lab.com](mailto://n.fridman@burning-lab.com)**