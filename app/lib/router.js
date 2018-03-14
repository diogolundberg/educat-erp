import Vue from "vue";
import VueRouter from "vue-router";

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
  ],
});

router.beforeEach((to, from, next) => {
  if (!localStorage.token && !to.meta.open) {
    next({ path: "/" });
  } else {
    next();
  }
});

export default router;
