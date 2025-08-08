## About

This package provides basic 3D interaction wrappers with events such as Hover, UnHover and Interact.
The interaction logic via raycasts targeted at colliders is included as well, though you need to implement your own input mechanics to trigger the interactions 
and update the hover callbacks at your own pace/ liking.

Checkout the [Features](#features) section below for more information about the specific use cases and available helper scripts.

## Support

Since I am developing and maintaining this asset package in my spare time, feel free to support me [via paypal](https://paypal.me/NikosProjects), [buy me a coffee](https://ko-fi.com/nikocreatestech) or check out my [games](https://store.steampowered.com/curator/44541812) or other [published assets](https://assetstore.unity.com/publishers/52812).

## Documentation

See the API doc [TBA]

## Setup

### Unity Package Dependency

To add this toolkit as a package dependency to your Unity project, locate your manifest file in "Package/manifest.json" or add the git-url via the package manager UI.

In the previous versions of this package you had to add the NaughtyAttributes package dependency to the "scopedRegistries". Unfortunately this forced you to use a specific fork or version, so to avoid that restriction you have to add the NaughtyAttributes git url (fork/ version) of your liking yourself.

The current dependency is a fork with performance improvements ([https://github.com/niggo1243/NaughtyAttributes](https://github.com/niggo1243/NaughtyAttributes)) of the original open-source project NaughtyAttributes by dbrizov: [https://github.com/dbrizov/NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)

The original NaughtyAttributes package works as well though and if you already have it installed, you don't have to add the forked branch in the following steps!

Add the following lines to the "dependencies" section to include this package, the required pausehandler package and my helpers package dependency (scopedRegistries automatic dependency resolve setup is in progress!):
```
"com.nikosassets.helpers": "https://github.com/niggo1243/Unity3DHelperTools.git#upm"
"com.nikosassets.pausehandler": "https://github.com/niggo1243/NikosAssets.PauseHandler.git"
"com.nikosassets.interactions": "https://github.com/niggo1243/NikosAssets.Interactions.git"
```

For my NaughtyAttributes performance improvements fork:
```
"com.nikosassets.naughtyattributes": "https://github.com/niggo1243/NaughtyAttributes.git#upm"
```

The original branch:
```
"com.dbrizov.naughtyattributes": "https://github.com/dbrizov/NaughtyAttributes.git#upm"
```

You can also choose specific releases and tags after the "#" instead of "upm".

## Features

In depth sample scenes & feature showcase with pictures/ gifs [TBA].

In short:

- The ```BaseInteractable``` is the object you want to interact with. Inherit from this and/ or subscribe to the interaction events (```OnInteracted```, ```OnHovered```, ```OnUnhovered```) to execute your custom logic.
- The ```InteractableGroup``` redirects the interaction calls to it's assigned ```BaseInteractable```s.
- If you want to interact with a ```BaseInteractable``` directly, it must have a collider alongside this component!
- To actally call the hover and interaction functions, you must add the ```BaseRaycastingPlayer``` component to your player and call it's ```HandleHover()``` function repeatedly (for example every Update or FixedUpdate frame)
and the ```Interact()``` function whenever an interaction should or could happen, for example when pressing the left mouse button.

