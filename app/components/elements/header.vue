<template>
  <div>
    <header class="fixed top-0 z3 w100 shadow1 bg-green-grad white">
      <div class="max-width-4 m-auto flex items-center justify-between p2">
        <div class="flex-auto flex items-center">
          <div class="h3">CMMG</div>
          <h2 class="h2 my0 mx2 px2 thin border-white-50 border-left">
            Matrícula Online
          </h2>
        </div>

        <slot />

        <div>
          <a
            class="pl2 pointer"
            href="javscript:void(0)"
            @click.prevent="$emit('notifications')">
            <Icon name="bell" />
          </a>
          <router-link
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
  };
</script>
