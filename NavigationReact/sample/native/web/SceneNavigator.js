import {Motion, TransitionMotion} from 'react-motion';
import React, {Component} from 'react';

class SceneNavigator extends Component{
    constructor(props) {
        super(props);
        var {state, data, url} = props.stateNavigator.stateContext;
        this.state = {scenes: {[url]: state.renderScene(data)}};
    }
    componentDidMount() {
        var {stateNavigator} = this.props;
        stateNavigator.onNavigate((oldState, state, data, asyncData) => {
            this.setState((prevState) => {
                var {url, crumbs} = stateNavigator.stateContext;
                var scenes = {[url]: state.renderScene(data, asyncData)};
                for(var i = 0; i < crumbs.length; i++) {
                    scenes[crumbs[i].url] = prevState.scenes[crumbs[i].url]; 
                }
                return {scenes};
            });
        });
    }
    renderMotion({key, data: {scene, state, data, mount}, style: {leave,...style}}) {
        var {getUnmountedStyle, getMountStyle, getMountedStyle, interpolateStyle} = this.props;
        return <Motion key={key} defaultStyle={getUnmountedStyle(state, data)}
            style={(leave ? style : (mount ? getMountStyle : getMountedStyle)(state, data))}>
            {(interpolatingStyle) => <div style={interpolateStyle(interpolatingStyle)}>{scene}</div>}
        </Motion>;
    }
    render() {
        var {oldState, state, data, url, crumbs} = this.props.stateNavigator.stateContext;
        var {getMountStyle, getUnmountStyle} = this.props;
        var sceneContexts = crumbs.concat({state, data, url, mount: true});
        return <TransitionMotion willLeave={() => ({...getUnmountStyle(state, data), leave: 1})}
            styles={sceneContexts.map(({state, data, url, mount}) => ({
                key: url,
                data: {scene: this.state.scenes[url], state, data, mount},
                style: {...getMountStyle(state, data), leave: 0}
            }))}>
            {interpolatedStyles =>
                <div>{interpolatedStyles.map((config) => this.renderMotion(config))}</div>}
        </TransitionMotion>;
    }
}

export default SceneNavigator;
