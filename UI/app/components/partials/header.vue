<template>
  <div>
    <header class="fixed top-0 left-0 z3 w100 bg-green-grad white">
      <div class="max-width-4 m-auto flex items-center justify-between p1">
        <div
          v-if="menu"
          class="m1 pointer sm-hide md-hide lg-hide"
          @click="$emit('sidebar')">
          <Icon
            :width="32"
            name="menu" />
        </div>
        <div class="flex-auto flex items-center xs-hide">
          <div>
            <img
              src="../../assets/img/logo.svg"
              :style="{ height: '3rem' }">
          </div>
          <h2 class="my0 mx2 px2 thin border-white-50">
            Matrícula Online
          </h2>
        </div>

        <div class="sm-hide">
          <slot />
        </div>

        <div>
          <a
            class="pl2 pointer"
            href="javscript:void(0)"
            @click.prevent="$emit('notifications')">
            <Icon name="bell" />
          </a>
          <router-link
            v-if="$store.getters.logged"
            class="pl2 pointer"
            to="/logout">
            <Icon name="logout" />
          </router-link>
        </div>
      </div>
    </header>
    <div class="m3 p1" />
  </div>
</template>

<script>
  export default {
    name: "Header",
    props: {
      menu: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
