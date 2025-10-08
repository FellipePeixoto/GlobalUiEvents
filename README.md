# Unity Package Readme Template

Listens for UI events triggered by user navigation only (Move, Click).

## Features

- Play sounds when the user navigates through the interface
- Create presets for each group of selectables
- Trigger an event when any listened selectable is selected
- Update the listened selectables in runtinme calling `AddNewSelectables`. (if they are destroyed later `CleanUpSelectables` must be called after destruction)

## Installation

Add this Git URL as a package:
<pre>https://github.com/FellipePeixoto/GlobalUiEvents.git</pre>

## Usage

1. Add the `SelectablesListenerManager` to a GameObject with a `RectTransform`. Any selectable child under that GameObject’s hierarchy will be detected (Move, Click), and the corresponding sounds will be played.  
   ![Add Component](/Docs~/Unity_64sX15Yxf2.gif)
2. Create an **Audio Preset ScriptableObject** to define sounds for UI events like Click and Move.  
   In the Project window, right-click and select:  
   `DevPeixoto > Global UI Events > Audio Preset`
3. Assign the preset in the `SelectablesListenerManager` inspector:  
   ![Search Prop field](/Docs~/Unity_Zo2QbjuBou.gif)  
   or drag the created preset directly into the **Audio Preset** field.  
   ![Drag SO](/Docs~/Unity_asaVSgsdw8.gif)

## Requirements

- Tested on Unity 6000.0.38.

## Known Issues

- This package was created by extracting generic functionality from my games. It met my needs but may contain bugs. If you find an issue, feel free to report it and I’ll address it when possible.

## Contributing

Contributions are welcome! Open an issue or submit a pull request.

## Future Roadmap

- [ ] Allow creating a new preset directly from the inspector
- [ ] Positive/Negative sounds for Up and Down or Right and Left
- [ ] Positive/Negative sounds for Right and Left on Slide
- [ ] To separeted sounds after slider meets it's lowest or higher values
- [ ] Decouple Play Audio `SelectablesListenerManager` to enable take advantage from other audio system solutions

## License

MIT License
