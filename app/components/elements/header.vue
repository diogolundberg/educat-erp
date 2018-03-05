<template>
  <div>
    <header class="fixed top-0 z3 w100 p2 bg-white black shadow0 flex
      justify-between">
      <h1 class="m0 h3">Matrícula</h1>
      <img
        class="y2 pointer"
        src="../../assets/img/bell.svg"
        @click="$emit('notifications')">
    </header>
    <div class="p2 mb4" />
  </div>
</template>

<script>
  export default {
    name: "Header",
  };
</script>
