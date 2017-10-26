import * as angular from 'angular'
import '@uirouter/angularjs'
import 'angular-material'
import 'angular-material/angular-material.css'

require('./app.scss');

export const Module = angular
    .module('AzetsApp', ['ngMaterial', 'ui.router'])
    .config(appConfig);

appConfig.$inject = ['$urlRouterProvider', '$locationProvider'];
function appConfig(
    $urlRouterProvider: ng.ui.IUrlRouterProvider,
    $locationProvider: ng.ILocationProvider,
) {
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/');
}

import './components'
import './directives'
import './services'