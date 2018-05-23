<template>
  <div class="progress">
    <div
      :style="{ width: `${value / max * 100}%` }"
      class="bg-blue h100" />
  </div>
</template>

<script>
  export default {
    name: "Progress",
    props: {
      value: {
        type: Number,
        default: 0,
      },
      max: {
        type: Number,
        default: 100,
      },
    },
  };
</script>
