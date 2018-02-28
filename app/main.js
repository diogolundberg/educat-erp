import Vue from "vue";
import VueRouter from "vue-router";
import * as Components from "./components/**/*.vue";

Vue.use(VueRouter);
Object.values(Components).map(a => a.name && Vue.component(a.name, a));

const router = new VueRouter({
  mode: "history",
  routes: [
    { path: "/", component: Vue.component("App") },
  ],
});

new Vue({ render: h => h("Application"), router }).$mount("main");
