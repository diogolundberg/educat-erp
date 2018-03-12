<template>
  <button
    :class="{
      'btn-primary': primary,
      'btn-primary bg-red': danger,
      'btn-primary bg-yellow': warning,
      'btn-primary bg-green': success,
    }"
    :type="submit ? 'submit' : 'button'"
    class="btn upcase shadow0 rounded ease center"
    @click="$emit('click')">
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
      submit: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
