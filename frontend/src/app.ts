import * as angular from 'angular';
import '@uirouter/angularjs';
import 'angular-material';
import 'angular-material/angular-material.css';
import 'ng-file-upload';
import 'angular-messages';

require('./app.scss');

export const Module = angular
    .module('AzetsApp', ['ngMaterial', 'ui.router', 'ngFileUpload', 'ngMessages'])
    .config(appConfig)
    .run(appRun)
    .constant('config', {
        apiEndpoint: 'http://localhost:6001/'
    } as IConfig);

appConfig.$inject = ['$urlRouterProvider', '$locationProvider'];
function appConfig(
    $urlRouterProvider: ng.ui.IUrlRouterProvider,
    $locationProvider: ng.ILocationProvider,
) {
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/');
}

appRun.$inject = ['$q'];
function appRun(
    $q: ng.IQService,
) {
    (window as any).Promise = $q;
}

import './components'
import './directives'
import './services'
import './filters'