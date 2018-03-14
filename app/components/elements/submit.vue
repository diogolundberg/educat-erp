<template>
  <Btn
    primary
    submit
    :disabled="loading"
    @click="click">
    {{ loading ? "..." : label }}
    <slot />
  </Btn>
</template>

<script>
  export default {
    name: "Submit",
    props: {
      label: {
        type: String,
        required: false,
        default: null,
      },
    },
    data() {
      return {
        loading: false,
      };
    },
    methods: {
      async click() {
        this.loading = true;
        await this._events.click[0]();
        await this.sleep(500);
        this.loading = false;
      },
    },
  };
</script>
