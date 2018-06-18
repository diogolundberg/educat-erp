<template>
  <div
    :class="{
      'inline-block': inline,
      'x3 y3 p0 p2x m1': small,
      'x4 y4 p10x': !small,
      'bg-white black': white,
      'bg-gray white': !white,
    }"
    class="circle bold center shadow1">
    <div class="h2">
      <slot />
      {{ content }}
    </div>
  </div>
</template>

<script>
  export default {
    name: "Ball",
    props: {
      content: {
        type: [Number, String],
        required: false,
        default: null,
      },
      blank: {
        type: Boolean,
        default: false,
        required: false,
      },
      inline: {
        type: Boolean,
        default: false,
        required: false,
      },
      small: {
        type: Boolean,
        default: false,
        required: false,
      },
      white: {
        type: Boolean,
        default: false,
        required: false,
      },
    },
  };
</script>
