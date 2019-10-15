import Vue from 'vue';
import VueRouter from 'vue-router';

import HomePage from '../components/HomePage';
import AdminHome from '../components/AdminHome';

Vue.use(VueRouter);

export default new VueRouter({
    mode: "history",
    routes: [
        { path: "/", component: HomePage },
        { path: "/admin", component: AdminHome },
        { path: "*", redirect: "/" }
    ]
})