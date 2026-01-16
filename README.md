## About

This package provides basic 3D interaction wrappers with events such as Hover, UnHover and Interact.
The interaction logic via raycasts targeted at colliders is included as well, though you need to implement your own input mechanics to trigger the interactions 
and update the hover callbacks at your own pace/ liking.

Checkout the [Features](#features) section below for more information about the specific use cases and available helper scripts.

## Support

Since I am developing and maintaining this asset package in my spare time, feel free to support me [via paypal](https://paypal.me/NikosProjects), [buy me a coffee](https://ko-fi.com/nikocreatestech) or check out my [games](https://store.steampowered.com/curator/44541812) or other [published assets](https://assetstore.unity.com/publishers/52812).

## Setup

### Unity Package Dependency

The current dependency is a fork with performance improvements ([https://github.com/nikodemgrz/NaughtyAttributes](https://github.com/nikodemgrz/NaughtyAttributes)) of the original open-source project NaughtyAttributes by dbrizov: [https://github.com/dbrizov/NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)

The original NaughtyAttributes package is compatible with this package in case you already have it installed.

Add the following git urls in the Unity PackageManager:
```
"https://github.com/nikodemgrz/Unity3DHelperTools.git#upm"
"https://github.com/nikodemgrz/NikosAssets.PauseHandler.git"
"https://github.com/nikodemgrz/NikosAssets.Interactions.git"
```

For my NaughtyAttributes performance improvements fork:
```
"https://github.com/nikodemgrz/NaughtyAttributes.git#upm"
```

The original branch:
```
"https://github.com/dbrizov/NaughtyAttributes.git#upm"
```

You can also choose specific releases and tags after the "#" instead of "upm".

## Features

In short:

- The ```BaseInteractable``` is the object you want to interact with. Inherit from this and/ or subscribe to the interaction events (```OnInteracted```, ```OnHovered```, ```OnUnhovered```) to execute your custom logic.
- The ```InteractableGroup``` redirects the interaction calls to it's assigned ```BaseInteractable```s.
- If you want to interact with a ```BaseInteractable``` directly, it must have a collider alongside this component!
- To actally call the hover and interaction functions, you must add the ```BaseRaycastingPlayer``` component to your player and call it's ```HandleHover()``` function repeatedly (for example every Update or FixedUpdate frame)
and the ```Interact()``` function whenever an interaction should or could happen, for example when pressing the left mouse button.

