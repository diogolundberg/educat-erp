<template>
  <div>
    <Alert
      v-for="error in value"
      :key="error"
      :message="error"
      :error="!success" />
  </div>
</template>

<script>
  export default {
    name: "BaseErrors",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      success: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
