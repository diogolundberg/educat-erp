import Vue from "vue";
import VueRouter from "vue-router";
import * as Components from "./components/**/*.vue";

Vue.use(VueRouter);
Object.values(Components).map(a => a.name && Vue.component(a.name, a));

const router = new VueRouter({
  mode: "history",
  routes: [
    { path: "/", component: Vue.component("App") },
    { path: "/list", component: Vue.component("Enrollments") },
    { path: "/style", component: Vue.component("StyleGuide") },
  ],
});

new Vue({ render: h => h("Application"), router }).$mount("main");
