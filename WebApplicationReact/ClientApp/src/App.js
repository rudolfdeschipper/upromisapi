import React, { Component, Suspense, Promise } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
//import { NotFoundPage } from './components/NotFoundPage';

export default class App extends Component {
    static displayName = App.name;

    render() {
        const Contract = React.lazy(() => import("./components/Contract"));
        return (
            <Layout>
                <Suspense fallback={<div className="page-container">Loading...</div>}>
                    <Switch>
                        <Route exact path='/' component={Home} />
                        <Route path='/home' component={Home} />
                        <Route path='/counter' component={Counter} />
                        <Route path='/contract' component={Contract} />
                    </Switch>
                </Suspense>
            </Layout>
        );
    }
}
