<template>
  <button
    :class="{
      'btn-primary': primary,
      'btn-primary bg-red': danger,
      'btn-primary bg-yellow': warning,
      'btn-primary bg-green': success,
      'btn-primary bg-yellow-dim green': yellow,
      'btn-big': big,
      'circle x4 y4': fab,
      'fixed m3 bottom-0 right-0 z2 shadow2': fixed,
    }"
    :type="submit ? 'submit' : 'button'"
    class="btn upcase shadow0 rounded ease center no-select"
    @click.prevent="$emit('click')">
    {{ label }}
    <slot />
  </button>
</template>

<script>
  export default {
    name: "Btn",
    props: {
      label: {
        type: String,
        required: false,
        default: "",
      },
      primary: {
        type: Boolean,
        default: false,
      },
      danger: {
        type: Boolean,
        default: false,
      },
      warning: {
        type: Boolean,
        default: false,
      },
      success: {
        type: Boolean,
        default: false,
      },
      yellow: {
        type: Boolean,
        default: false,
      },
      fab: {
        type: Boolean,
        default: false,
      },
      fixed: {
        type: Boolean,
        default: false,
      },
      big: {
        type: Boolean,
        default: false,
      },
      submit: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
