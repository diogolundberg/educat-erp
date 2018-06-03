<template>
  <ScaleTransition>
    <div
      class="fixed fill bg-dim ease flex z4 overflow-scroll"
      @click.self="$emit('hide')">
      <div class="p2 m-auto rounded shadow2 bg-white width-4 bounce">
        <slot />
      </div>
    </div>
  </ScaleTransition>
</template>

<script>
  export default {
    name: "Modal",
  };
</script>
