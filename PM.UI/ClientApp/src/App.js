import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Members } from './components/Members';
import moment from 'moment'

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/members' component={Members} />
                {moment().format("MMMM Do YY")}{""}
            </Layout>
        );
    }
}
