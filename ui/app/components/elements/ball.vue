<template>
  <div
    :class="{ 'inline-block': inline }"
    class="x4 y4 p10x circle bold bg-gray white center shadow1">
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
    },
  };
</script>
