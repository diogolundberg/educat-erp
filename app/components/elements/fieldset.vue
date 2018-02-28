<template>
  <div>
    <template v-if="title">
      <h3 class="thin">
        {{ title }}
      </h3>
    </template>
    <div
      :class="innerClass"
      class="py2">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Fieldset",
    props: {
      title: {
        type: String,
        required: false,
        default: null,
      },
      innerClass: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
