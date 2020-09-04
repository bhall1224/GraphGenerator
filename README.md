# Utilities
## A basic class library for using in Unity projects
Clone this library, build the .dll file and include in your Unity .csproj file class references, or simply copy and paste into your code directly.  

*Example usage*:
`BasicController.TopDownTouchScreenControlHandler(
                PlayerJoystick.Horizontal,
                PlayerJoystick.Vertical,
                AttackButton.Pressed,
                out _displacementVector,
                out _rotationalVector,
                _player.transform,
                ActionCallback,
                AnimationCallback);`
                
In this snippet the controller takes Horizontal and Vertical magnitudes as a `float`, a  `bool` for the action button.  We're also outputting vectors for
displacement and rotation vectors.  Optionally you can pass in the given Transform of the controlled GameObject, and callback functions for a given action
or animation
