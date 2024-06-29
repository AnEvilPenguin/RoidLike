# RoidLike

RoidLike is a quick proof of concept for a number of different ideas.
As such it's not the greatest implementation of any of them...

- First Game using Unity
- First use of 2d projectiles and proper physics (minus gravity)
- First attempt at Rogue Like/Lite upgrade systems
- First attempt at increasing difficulties
- First use of Events outside of basic UI buttos and the like

It takes the form of an Asteroids clone with some very basic upgrade options.

## Controls

Left/Right arrows to rotate
Up arrow to accelerate in current direction
Space to shoot

Mouse to select upgrades and interact with basic menu.

# Conclusion

The main objective was to get a basic game together from scratch using Unity. This was successful.

2D projectiles and physics was simpler than I expected in some places and more complex in others. Perhaps naively I expected vectors to be fairly easy to rotate, when they aren't. Though in the process I've learned the very basics of Quaternions. Firing projectiles I was expecting to be more diffcult than just shoving them!

The Rogue Like/Lite system worked more or less as I planned. It's definitely a very simple implementation, but this could easily be expanded. I may still need to look at implementing some complexity into this. Factory/Decorator patterns may be useful for things like enemy/weapon upgrades. These patterns may also be useful for things like generating upgrades with rarity based on player luck, and so on.

increasing difficulty was handled through by Asteroid Factory, which progressively increased the minimum number of asteroids per level. It should be fairly easy to implement other difficulty scaling options here as well. Though it does feel like I might be missing a better option for this in Unity. I still need to investigate if I can use a template to instantiate from, or if indeed the GameObject prefab is an in place modifyable template as is.

Events worked more or less how I expected them to, and feels like a fairly good way of propagating data around. The main Issue I ran into was the use of a static class event, when destroying and reloading the scene this was leaving null references behind. This was easily fixed by unsubscribing the listeners on Destroy, and could have been avoided entirely by using instanced events. Using Instanced events would have increased the complexity slightly, but probably not enough to worry about. The GameMaster class was (successfully) used as a mediator here, which should not meaninfully change if moving to instance based.
