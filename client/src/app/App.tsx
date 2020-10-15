import 'office-ui-fabric-react/dist/css/fabric.css';

import { IButtonProps, Icon, Image, initializeIcons, Nav, Text } from '@fluentui/react';
import About from 'app/pages/about/About';
import Groups from 'app/pages/groups/Groups';
import Home from 'app/pages/home/Home';
import msftLogo from 'app/static/msftLogo.png';
import React from 'react';
import { BrowserRouter, Link, Route } from 'react-router-dom';

import * as serviceWorker from '../serviceWorker';
import styles from './App.module.scss';

initializeIcons();

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <React.Fragment>
                <div className="ms-Grid" dir="ltr">
                    <div className="ms-Grid-row">
                        <div className={styles.header}>
                            <Image height={30} src={msftLogo} className={styles.msftLogo} />
                            <div className={styles.headerDivider} />
                            <Text className={styles.headerTitle}>My First React App</Text>
                        </div>
                        <div className={'ms-Grid-col ms-sm12 ms-lg4 ms-xl2'}>
                            <Nav
                                linkAs={onRenderLink}
                                groups={[
                                    {
                                        collapseAriaLabel: 'Collapse',
                                        expandAriaLabel: 'Expand',
                                        links: [
                                            {
                                                name: 'Home',
                                                url: '/',
                                                key: 'home'
                                            },
                                            {
                                                name: 'About',
                                                url: '/about',
                                                key: 'about'
                                            },
                                            {
                                                name: 'Groups',
                                                url: '/groups',
                                                key: 'groups'
                                            }
                                        ]
                                    }
                                ]}
                            />
                        </div>
                        <div className="ms-Grid-col ms-sm12 ms-lg8 msxl-10">
                            <Route path="/" exact component={Home} />
                            <Route path="/about" component={About} />
                            <Route path="/groups" component={Groups} />
                        </div>
                    </div>
                </div>
            </React.Fragment>
        </BrowserRouter>
    );
};

// custom component to make react-router-dom Link component work in fabric Nav
const onRenderLink = (props: IButtonProps) => {
    return (
        <Link className={props.className} style={{ color: 'inherit', boxSizing: 'border-box' }} to={props.href}>
            <span style={{ display: 'flex' }}>
                {!!props.iconProps && <Icon style={{ margin: '0 4px' }} {...props.iconProps} />}
                {props.children}
            </span>
        </Link>
    );
};

export default App;

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
