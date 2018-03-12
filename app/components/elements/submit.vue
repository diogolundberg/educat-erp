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
      click() {
        this.loading = true;
        this._events.click[0]().then(() => setTimeout(() => {
          this.loading = false;
        }, 500));
      },
    },
  };
</script>
