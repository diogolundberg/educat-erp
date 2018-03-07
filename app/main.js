import Vue from "vue";
import VueRouter from "vue-router";
import * as Components from "./components/**/*.vue";

Vue.use(VueRouter);
Object.values(Components).map(a => a.name && Vue.component(a.name, a));

const router = new VueRouter({
  mode: "history",
  routes: [
    { path: "/", component: Vue.component("Login"), meta: { open: true } },
    { path: "/enroll", component: Vue.component("App"), meta: { nav: true } },
    { path: "/list", component: Vue.component("Enrollments") },
    { path: "/style", component: Vue.component("StyleGuide") },
    {
      path: "/logout",
      beforeEnter(to, from, next) {
        delete localStorage.token;
        next("/");
      },
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (to.path !== "/" && !localStorage.token) {
    next({ path: "/" });
  } else {
    next();
  }
});

new Vue({ render: h => h("Application"), router }).$mount("main");
