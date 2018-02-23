<template>
  <div class="progress">
    <div class="bg-blue h100" :style="{ width: `${value / max * 100}%` }" />
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
