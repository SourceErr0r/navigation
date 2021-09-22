import React from 'react'
import { requireNativeComponent, Platform } from 'react-native';

const CoordinatorLayout = ({overlap, children}) => (
    <NVCoordinatorLayout overlap={overlap} style={{flex: 1}}>{children}</NVCoordinatorLayout>
);
const NVCoordinatorLayout = requireNativeComponent<any>('NVCoordinatorLayout', null)

export default Platform.OS === 'android' ? CoordinatorLayout : ({children}) => children;

