<template>
  <div
    :class="{ 'bg-blue white': !blank, 'shadow0': blank }"
    class="x2 y2 p4x circle bold h4 center">
    {{ index }}
  </div>
</template>

<script>
  export default {
    name: "Ball",
    props: {
      index: {
        type: [Number, String],
        required: true,
      },
      blank: {
        type: Boolean,
        default: false,
        required: false,
      },
    },
  };
</script>
