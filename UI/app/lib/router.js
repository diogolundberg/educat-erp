import Vue from "vue";
import VueRouter from "vue-router";

import store from "./store";

const lazy = name => () => Promise.resolve(Vue.component(name));

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes: [
    {
      path: "/",
      component: lazy("Login"),
      meta: { open: true },
    },
    {
      path: "/enroll/:id",
      component: lazy("Enrollment"),
      meta: { open: true },
      props: true,
    },
    {
      path: "/list",
      component: lazy("Enrollments"),
    },
    {
      path: "/style",
      component: lazy("StyleGuide"),
    },
    {
      path: "/logout",
      beforeEnter(to, from, next) {
        delete localStorage.token;
        next("/");
      },
    },
    {
      path: "*",
      component: lazy("NotFound"),
      meta: { open: true },
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (!store.getters.logged && !to.meta.open) {
    next({ path: "/" });
  } else {
    next();
  }
});

export default router;
