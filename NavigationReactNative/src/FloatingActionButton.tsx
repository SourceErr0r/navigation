import React from "react";
import { Image, Platform, requireNativeComponent, Animated } from "react-native";

class FloatingActionButton extends React.Component<any, any> {
    render() {
        var { image, ...props } = this.props;
        return (
            <NVFloatingActionButton
                image={Image.resolveAssetSource(image)}
                {...props}
            />
        );
    }
}

const NVFloatingActionButton = requireNativeComponent<any>("NVFloatingActionButton", null);

export default Platform.OS === 'android' ? Animated.createAnimatedComponent(FloatingActionButton) : null;
