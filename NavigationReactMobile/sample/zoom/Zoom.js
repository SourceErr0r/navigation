import React from 'react';
import {NavigationMotion} from 'navigation-react-mobile';
import ZoomShared from './ZoomShared';

export default ({app}) => (
  <NavigationMotion
    app={app}
    unmountedStyle={{opacity: 0}}
    mountedStyle={{opacity: 1}}
    crumbStyle={{opacity: 0}}
    sharedElementMotion={props => <ZoomShared {...props} />}>
    {({opacity}, scene, key) => (
      <div key={key}
        className="scene"
        style={{opacity}}>
        {scene}
      </div>
    )}
  </NavigationMotion>
);
