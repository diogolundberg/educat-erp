<template>
  <div>
    <Alert
      v-for="error in value"
      :key="error"
      :message="error"
      error />
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
    },
  };
</script>
